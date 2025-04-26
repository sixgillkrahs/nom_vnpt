import { Component, HostListener, OnInit } from '@angular/core';
import { LocalizationService, NotificationService } from '@ctincsp/ng.core';
import { ProjectGeneralService } from '@proxy/project-generals';
import { ProjectGeneralDto, ProjectGeneralGetListInput } from '@proxy/project-generals/dtos';
import { ProjectStateService } from '@proxy/project-state';
import { GetAllProjectStateDto } from '@proxy/project-state/dtos';
import { ConfirmationService } from 'primeng/api';
import { BehaviorSubject } from 'rxjs';
import { ActiveIndex } from 'src/app/shared/ProjectManager/enums/activeIndex.enum';
import { ProjectDetailData } from 'src/app/shared/ProjectManager/models/project-manager.model';


@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent implements OnInit {

  isLoading: boolean = false;

  projects: ProjectGeneralDto[] = [];

  projectFilter: ProjectGeneralGetListInput;

  selectedProject: any[] = [];

  selectedProjects!: any[] | null;

  stateOptions: any[] = [];

  startDate: any;
  endDate: any;

  displayProjectDetail: boolean = false;
  displayProjectContainer: boolean = false;

  projectDetailData: ProjectDetailData;

  keyword: any;

  pmsProjectCode: string;
  isSyncing: boolean = false;

  mediumSide: boolean = window.innerWidth < 600 ? false : true;

  smallSide: boolean = window.innerWidth < 600 ? true : false;

  public totalCount: number;
  skipCount = 0;
  maxResultCount = 10;

  constructor(
    private localizationService: LocalizationService,
    private projectGeneralService: ProjectGeneralService,
    private confirmationService: ConfirmationService,
    private notificationService: NotificationService,
    private projectStateService: ProjectStateService
  ) { }

  ngOnInit() {
    this.getProject();
    this.getProjectStateDropdown();
  }

  actionItems: any[] = [];
  dropdownItemsButton(project) {
    this.actionItems = [
      {
        label: this.localizationService.instant("Work::View"),
        icon: 'pi pi-eye',
        command: () => { this.onView(project) }
      },
      {
        label: this.localizationService.instant("Work::Edit"),
        icon: 'pi pi-pencil',
        command: () => { this.showModal(project.id) }
      },
      { separator: true },
      {
        label: this.localizationService.instant("Work::Delete"),
        icon: "pi pi-trash",
        command: () => { this.confirmDeleteProject(project); },
      },
    ]
  }

  getProject() {
    this.isLoading = true;

    this.projectFilter = {
      skipCount: this.skipCount,
      maxResultCount: this.maxResultCount,
      filter: this.keyword,
    }

    console.log(this.projectFilter);

    this.projectGeneralService.getList(this.projectFilter).subscribe({
      next: (project) => {
        this.isLoading = false;
        this.projects = project.items;
        this.totalCount = project.totalCount;

        project.items.forEach(p => {
          if (p.status) {
            this.selectedProject.push(p)
          }
        });
      },
      error: (err) => {
        this.isLoading = false;
      }
    })
  }

  onPageChange(event) {
    this.skipCount = event.page * this.maxResultCount;
    this.maxResultCount = event.rows;
    console.log(event);
    this.getProject();
  }

  getProjectStateDropdown() {
    let filterState: GetAllProjectStateDto = {
      filter: null,
      sorting: null
    }
    this.isLoading = true;
    this.projectStateService.getDropdown(filterState).subscribe({
      next: (res) => {
        this.stateOptions = res;
        this.isLoading = false;
      },
      error: (err) => {
        this.isLoading = false;
      }
    })
  }

  onView(project: any) {
    this.displayProjectContainer = true;
    const data = {
      header: this.localizationService.instant('Work::Pm:Project.Detail'),
      active: ActiveIndex.Detail,
      id: project.id
    };
    this.projectDetailData = data;
  }

  showModal(id?: any) {
    this.displayProjectDetail = true;
    const data: ProjectDetailData = {
      header: this.localizationService.instant('Work::Pm:Project.New'),
      active: ActiveIndex.Detail,
    };
    if (id) {
      data.id = id;
      data.header = this.localizationService.instant('Work::Pm:Project.Detail');
    }
    this.projectDetailData = data;
  }

  syncProject() {
    if (this.pmsProjectCode) {
      this.isSyncing = true;
      this.projectGeneralService.syncProject(this.pmsProjectCode).subscribe(res => {
        this.isSyncing = false;
        this.getProject();
        this.notificationService.showSuccess(this.localizationService.instant("Work::Message:Sync.Ok", this.pmsProjectCode));
        this.pmsProjectCode = "";
      }, err => {
        this.isSyncing = false;
      });
    } else {
      this.notificationService.showWarn("Project Code Required!");
    }
  }

  confirmDeleteProject(project: ProjectGeneralDto) {
    this.confirmationService.confirm({
      message: this.localizationService.instant('Work::Message:Confirm.Delete', project.name),
      accept: () => {
        this.deleteProject(project);
      },
    });
  }

  deleteProject(project: ProjectGeneralDto) {
    this.projectGeneralService.delete(project.id).subscribe({
      next: (res) => {
        this.notificationService.showSuccess(this.localizationService.instant("Work::Message:Delete.Ok", project.name));
        this.getProject();
      },
      error: (err) => {
        console.log(err);
      }
    })
  }

  closeProjectDetail(event) {
    if (event) {
      this.getProject();
      this.displayProjectDetail = false;
    } else {
      this.displayProjectDetail = false;
    }
  }

  closeProjectContainer(event) {
    if (event) {
      this.getProject();
      this.displayProjectContainer = false;
    } else {
      this.displayProjectContainer = false;
    }
  }

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    if (window.innerWidth < 600) {
      this.smallSide = true;
      this.mediumSide = false;
    } else {
      this.smallSide = false;
      this.mediumSide = true;
    }
  }
}
