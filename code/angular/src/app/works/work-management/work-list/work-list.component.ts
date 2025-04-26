import {
  ConfigStateService,
  LocalizationService,
  NotificationService,
  PermissionService,
} from '@ctincsp/ng.core';
import { ChangeDetectorRef, Component, Input, OnDestroy, OnInit } from '@angular/core';
import { CspWorkDto, CspWorkGetListInput, CspWorkService } from '@proxy/work-management';
import { Subject, takeUntil } from 'rxjs';
import { ConfirmationService, TreeNode } from 'primeng/api';
import { CspCategoryService, DropdownItem, GetCSPCategoryDropdownDto } from '@ctincsp/ng.category';
import { ProjectGeneralService } from '@proxy/project-generals';
import { GetAllProjectGeneralDto } from '@proxy/project-generals/dtos';

@Component({
  selector: 'app-work-list',
  templateUrl: './work-list.component.html',
  styleUrls: ['./work-list.component.scss'],
})
export class WorkListComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  @Input() card: boolean = true;

  isLoading: boolean = false;

  works: CspWorkDto[] = [];
  treeData: TreeNode<CspWorkDto>[];
  catPriority: DropdownItem[] = [];
  filter: CspWorkGetListInput = {
    maxResultCount: 10,
    skipCount: 0,
    onlyRootNode: true,
    childIds: [],
    projectId: null,
  };
  public totalCount: number;

  hasPermissionUpdate = false;
  hasPermissionDelete = false;
  visibleActionColumn = false;

  displayWorkAdd: boolean = false;
  displayWorkViewMaster: boolean = false;
  displayHandOverWork: boolean = false;
  currentUser: any;
  actionItems: any[] = [];

  projectOptions: any;
  selectedItem: CspWorkDto;

  constructor(
    private config: ConfigStateService,
    private localizationService: LocalizationService,
    private cd: ChangeDetectorRef,
    private permissionService: PermissionService,
    private cspWorkService: CspWorkService,
    private cspCategoryService: CspCategoryService,
    private confirmationService: ConfirmationService,
    private notificationService: NotificationService,
    private projectGeneralService: ProjectGeneralService
  ) {}

  ngOnInit() {
    this.getProjects();
    this.getCurrUser();
    this.getCategory();
  }

  getCurrUser() {
    this.isLoading = true;
    this.config.getOne$('currentUser').subscribe(currentUser => {
      this.currentUser = currentUser;
      this.isLoading = false;
    });
  }

  dropdownItemsButton(work) {
    this.actionItems = [
      {
        label: this.localizationService.instant('Work::AddChild'),
        icon: 'pi pi-plus',
        command: () => {
          this.showAddModal(work);
        },
      },
      {
        label: this.localizationService.instant('Work::Detail'),
        icon: 'pi pi-eye',
        command: () => {
          this.masterView(work);
        },
      },
      {
        label: this.localizationService.instant('Bàn giao cv'),
        icon: 'pi pi-eye',
        command: () => {
          this.handOverWork();
        },
      },
      { separator: true },
      {
        label: this.localizationService.instant('Work::Delete'),
        icon: 'pi pi-trash',
        command: () => {
          this.deleteRow(work);
        },
      },
    ];
  }

  getCategory() {
    this.cspCategoryService
      .getListDropdown({
        categoryGroup: 'Work_Cat_Priority',
      } as GetCSPCategoryDropdownDto)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(res => {
        this.catPriority = res;
      });
  }

  loadData() {
    debugger
    // If filter != null then only load root
    this.filter.onlyRootNode = !(
      this.filter.keyword ||
      this.filter.projectId ||
      this.filter.priority
    );
    this.isLoading = true;
    this.cspWorkService
      .getList(this.filter)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(res => {
        this.treeData = res.items.map(x => {
          let work = {
            ...x,
            dueDate: x.dueDate ? new Date(x.dueDate) : null,
            startDate: x.startDate ? new Date(x.startDate) : null,
          };
          return this.convertJsonToTreeNode(work, false, false);
        });
        this.totalCount = res.totalCount;
        this.isLoading = false;
        this.cd.markForCheck();
      });
  }
  loadNodes(event) {
    // this.filter.keyword = event?.filters?.global?.value;
    this.loadData();
  }
  onNodeExpand(event: any) {
    if (!event?.node) return;
    this.isLoading = true;
    const node = event.node as TreeNode<CspWorkDto>;
    let parentWork = node.data as CspWorkDto;
    this.cspWorkService
      .getChildren(parentWork.id, false)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(res => {
        node.children = res.map(x => {
          let work = {
            ...x,
            dueDate: x.dueDate ? new Date(x.dueDate) : null,
            startDate: x.startDate ? new Date(x.startDate) : null,
          };
          return this.convertJsonToTreeNode(work, false, false);
        });
        this.treeData = [...this.treeData];
        this.isLoading = false;
      });
  }

  getPermission() {
    this.hasPermissionUpdate = this.permissionService.getGrantedPolicy('CspWork.Update');
    this.hasPermissionDelete = this.permissionService.getGrantedPolicy('CspWork.Delete');
    this.visibleActionColumn = this.hasPermissionUpdate || this.hasPermissionDelete;
  }

  pageChanged(event: any): void {
    this.filter.skipCount = event.page * this.filter.maxResultCount;
    this.filter.maxResultCount = event.rows;
    this.loadData();
  }

  showAddModal(work: CspWorkDto = null) {
    this.displayWorkAdd = true;
    this.selectedItem = work;
  }

  submitWorkAdd() {
    this.selectedItem = null;
    this.displayWorkAdd = false;
    this.loadData();
  }
  closeWorkAdd() {
    this.selectedItem = null;
    this.displayWorkAdd = false;
  }

  handOverWork() {
    this.displayHandOverWork = true;
  }

  closehandOverWork(event) {
    if (event) {
      // this.getWorkList();
      this.displayHandOverWork = false;
    } else {
      this.displayHandOverWork = false;
    }
  }

  masterView(work: CspWorkDto) {
    this.selectedItem = work;
    this.displayWorkViewMaster = true;
  }

  submitWorkUpdate(event) {
    this.selectedItem = null;
    this.displayWorkViewMaster = false;
    this.loadData();
  }

  closeWorkViewMaster(event) {
    this.selectedItem = null;
    this.displayWorkViewMaster = false;
  }

  convertJsonToTreeNode(
    input: any,
    expandedAll: boolean = false,
    leafAll: boolean | null = null
  ): TreeNode {
    let result: TreeNode;
    result = {
      label: input.name ?? '',
      data: input,
      expanded: expandedAll,
      key: input.id,
      leaf: leafAll != null ? leafAll : undefined,
    } as TreeNode;
    if (!input.children) return result;
    else result.children = input.children.map(x => this.convertJsonToTreeNode(x, expandedAll));

    return result;
  }

  deleteRow(item) {
    if (!item) {
      this.notificationService.showError(
        this.localizationService.instant('AbpIdentity::Message:NotChoseAnyItem')
      );
      return;
    }

    this.confirmationService.confirm({
      message: this.localizationService.instant(
        'AbpIdentity::RoleDeletionConfirmationMessage',
        item.name
      ),
      accept: () => {
        this.isLoading = true;
        this.cspWorkService
          .delete(item.id)
          .pipe(takeUntil(this.ngUnsubscribe))
          .subscribe({
            next: () => {
              this.notificationService.showSuccess(
                this.localizationService.instant('AbpIdentity::Message:DeleteOk')
              );
              this.isLoading = false;
              this.loadData();
            },
            error: () => {
              this.isLoading = false;
            },
          });
      },
    });
  }

  getPriorityName(value) {
    return this.catPriority.find(x => x.value == value)?.label;
  }

  getProjects() {
    this.isLoading = true;
    this.projectGeneralService
      .getDropdown({} as GetAllProjectGeneralDto)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        data => {
          this.projectOptions = data;
          this.isLoading = false;
        },
        () => {
          this.isLoading = false;
        }
      );
  }
  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
