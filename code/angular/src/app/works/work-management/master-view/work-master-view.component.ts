import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  ConfigStateService,
  LocalizationService,
  NotificationService,
  UtilityService,
} from '@ctincsp/ng.core';
import { ProjectGeneralService } from '@proxy/project-generals';
import { GetAllProjectGeneralDto } from '@proxy/project-generals/dtos';
import { CspWorkDto, CspWorkGetListInput, CspWorkService } from '@proxy/work-management';
import { ConfirmationService, MenuItem, TreeNode } from 'primeng/api';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-work-master-view',
  templateUrl: './work-master-view.component.html',
  styleUrls: ['./work-master-view.component.scss'],
})
export class WorkViewMasterComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  @Input() work!: CspWorkDto;
  @Input() currentUser: any;
  @Input() catPriority: any[] = [];
  @Input() displayWorkViewMaster: boolean = false;
  @Output() closeWorkViewMaster = new EventEmitter();
  @Output() submitWorkUpdate = new EventEmitter();

  form: FormGroup;
  isLoading = false;
  progress: number = 0;

  displayCollaborator: boolean = false;

  dragging: boolean = false;

  workChild: any[] = [
    {
      code: 1,
      name: 'abc',
      category: 'hehehee',
      quantity: 1,
    },
  ];

  items: MenuItem[] | undefined;
  works: CspWorkDto[] = [];
  treeData: TreeNode<CspWorkDto>[];
  filter: CspWorkGetListInput = {
    maxResultCount: 10,
    skipCount: 0,
    onlyRootNode: false,
    childIds: [],
    projectId: null,
    parentId: null,
  };

  totalCount = 0;
  projectOptions: any[] = [];
  displayWorkAdd: boolean = false;

  constructor(
    private fb: FormBuilder,
    private utilService: UtilityService,
    private localizationService: LocalizationService,
    private config: ConfigStateService,
    private cd: ChangeDetectorRef,
    private cspWorkService: CspWorkService,
    private confirmationService: ConfirmationService,
    private notificationService: NotificationService,
    private projectGeneralService: ProjectGeneralService
  ) {}

  get formControls() {
    return this.form.controls;
  }

  get CheckList() {
    return this.form.get('checkList') as FormArray;
  }

  ngOnInit() {
    this.buildMenu();
    this.buildForm();
    this.getProjects();
    this.getWork();
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

  getWork() {
    this.isLoading = true;
    this.cspWorkService
      .get(this.work.id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        res => {
          let dto = {
            ...res,
            startDate: new Date(res.startDate),
            dueDate: new Date(res.dueDate),
          };
          this.form.patchValue(dto);
          this.isLoading = false;
        },
        err => {
          this.isLoading = false;
        }
      );
  }
  getListChildrenWork() {
    this.filter.parentId = this.work.id;
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
    this.getListChildrenWork();
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

  save() {
    this.utilService.markAllControlsAsDirty([this.form]);
    if (this.form.invalid) {
      this.notificationService.showWarn(
        this.localizationService.instant('AbpIdentity::Message:FormInvalid')
      );
      return;
    }
    this.isLoading = true;
    this.cspWorkService
      .update(this.work.id, this.form.value)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        res => {
          this.isLoading = false;
          this.displayCollaborator = false;
          this.notificationService.showSuccess(
            this.localizationService.instant('AbpIdentity::Message:Updatek')
          );
          this.displayWorkViewMaster = false;
          this.submitWorkUpdate.emit(res);
        },
        err => {
          this.isLoading = false;
        }
      );
  }

  pageChanged(event: any): void {
    this.filter.skipCount = event.page * this.filter.maxResultCount;
    this.filter.maxResultCount = event.rows;
    this.getListChildrenWork();
  }
  buildMenu() {
    this.items = [
      {
        label: 'Chia sẻ',
        icon: 'pi pi-share-alt',
      },
      {
        label: 'Tài liệu',
        icon: 'pi pi-file',
      },
      {
        label: 'Người liên quan',
        icon: 'pi pi-tag',
        command: () => {
          this.openCollaborator();
        },
      },
      {
        label: 'Nhắc việc',
        icon: 'pi pi-share-alt',
      },
      {
        label: 'Công việc lặp lại',
        icon: 'pi pi-sync',
      },
      {
        label: 'Phê duyệt',
        icon: 'pi pi-user-plus',
      },
      {
        label: 'Lịch sử công việc',
        icon: 'pi pi-clock',
      },
    ];
  }

  buildForm() {
    this.form = this.fb.group({
      workCode: [null, [Validators.required, Validators.maxLength(128)]],
      name: [null, [Validators.required, Validators.maxLength(255)]],
      taskCode: [null, [Validators.required, Validators.maxLength(128)]],
      startDate: [null],
      dueDate: [null],
      progress: [0, [Validators.min(0), Validators.max(100)]],
      duration: [null, [Validators.pattern(/^\d*\.?\d+(h|d|w)$/)]],
      priority: [null, [Validators.required]],
      complexity: [null],
      degreeOfImportant: [null],
      description: [null, [Validators.required, Validators.maxLength(500)]],
      owner: [null, [Validators.required]],
      assigner: [null],
      performer: [null],
      collaborators: [null],
      projectId: [null, [Validators.required]],
      restrictComplete: [false],
      parentId: [null],
      checkList: this.fb.array([]),
      concurrencyStamp: [null],
    });
  }

  addCheckList() {
    this.CheckList.push(
      this.fb.group({
        title: [null, [Validators.required, Validators.maxLength(256)]],
        isCompleted: [false],
      })
    );
  }

  deleteCheckList(index) {
    this.CheckList.removeAt(index);
  }

  openCollaborator() {
    this.displayCollaborator = true;
  }
  submitCollaborator(event) {
    let colls = [...event];
    this.form.get('collaborators').setValue(colls.join(','));
    this.displayCollaborator = false;
  }
  closeCollaborator(event) {
    this.displayCollaborator = false;
  }

  onMouseDown(event: MouseEvent) {
    this.dragging = true;
    this.updateProgress(event);
  }

  onMouseMove(event: MouseEvent) {
    if (this.dragging) {
      this.updateProgress(event);
    }
  }

  onMouseUp() {
    this.dragging = false;
  }

  updateProgress(event: MouseEvent) {
    const progressBarWidth = document.querySelector('.p-progressbar').clientWidth;
    const clickX =
      event.clientX - document.querySelector('.p-progressbar').getBoundingClientRect().left;
    let percentage = Math.min(100, Math.max(0, (clickX / progressBarWidth) * 100));
    percentage = parseFloat(percentage.toFixed(2));
    this.progress = percentage;
  }

  showAddModal() {
    this.displayWorkAdd = true;
  }

  submitWorkAdd() {
    this.displayWorkAdd = false;
    this.getListChildrenWork();
  }
  closeWorkAdd() {
    this.displayWorkAdd = false;
  }
  getPriorityName(value) {
    return this.catPriority.find(x => x.value == value)?.label;
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
