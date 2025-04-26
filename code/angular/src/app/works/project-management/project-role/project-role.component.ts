import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { LocalizationService, NotificationService } from '@ctincsp/ng.core';
import { ProjectRoleService } from '@proxy/project-role';
import { GetListProjectRoleDto, ProjectRoleDto } from '@proxy/project-role/dtos';
import { ConfirmationService } from 'primeng/api';
import { Subject, takeUntil } from 'rxjs';
import { ProjectRoleData } from 'src/app/shared/ProjectManager/models/project-manager.model';

@Component({
  selector: 'app-project-role',
  templateUrl: './project-role.component.html',
  styleUrls: ['./project-role.component.scss']
})
export class ProjectRoleComponent implements OnInit {

  private ngUnsubscribe = new Subject();

  projectRoles: ProjectRoleDto[] = [];

  selectedCity: any;

  cities: any[];

  isLoading: boolean = false;

  projectRoleFilter: GetListProjectRoleDto;

  selectedProjectRole: any[] = [];

  selectedProjectRoles!: any[] | null;

  displayProjectRole: boolean = false;

  projectRoleData: ProjectRoleData;

  keyword: any;

  public totalCount: number;
  skipCount = 0;
  maxResultCount = 10;

  constructor(
    private projectRoleService: ProjectRoleService,
    private confirmationService: ConfirmationService,
    private notificationService: NotificationService,
    private localizationService: LocalizationService,
  ) { }

  ngOnInit() {

    this.getProjectRoles();

    this.cities = [
      { name: 'New York', code: 'NY' },
      { name: 'Rome', code: 'RM' },
      { name: 'London', code: 'LDN' },
      { name: 'Istanbul', code: 'IST' },
      { name: 'Paris', code: 'PRS' }
    ];

    // this.projectRoles.forEach(d => {
    //   if(d.status){
    //     this.selectedProjectRoles.push(d)
    //   }
    // });

  }

  actionItems: any[] = [];
  dropdownItemsButton(projectRole) {
    this.actionItems = [
      {
        label: this.localizationService.instant("Work::Edit"),
        icon: 'pi pi-pencil',
        command: () => { this.showModal(projectRole) }
      },
      { separator: true },
      {
        label: this.localizationService.instant("Work::Delete"),
        icon: "pi pi-trash",
        command: () => { this.confirmDelete(projectRole); },
      },
    ]
  }

  getProjectRoles() {
    this.isLoading = true;

    this.projectRoleFilter = {
      skipCount: this.skipCount,
      maxResultCount: this.maxResultCount,
      filter: this.keyword
    }

    this.projectRoleService.getList(this.projectRoleFilter)
      .subscribe({
        next: (projectRole) => {
          this.isLoading = true;
          this.projectRoles = projectRole.items;
          this.totalCount = projectRole.totalCount;

          projectRole.items.forEach(p => {
            if (p.status) {
              this.selectedProjectRole.push(p)
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
    this.getProjectRoles();
  }

  showModal(projectRole?: any) {
    this.displayProjectRole = true;

    let data: ProjectRoleData = {
      header: this.localizationService.instant('Work::Pm:ProjectRole.New')
    }

    if (projectRole) {
      data.id = projectRole.id;
      data.param = projectRole;
    }

    this.projectRoleData = data;
  }


  confirmDelete(projectRole: ProjectRoleDto) {
    this.confirmationService.confirm({
      message: this.localizationService.instant('Work::Message:Confirm.Delete', projectRole.name),
      accept: () => {
        this.deleteProjectRole(projectRole);
      },
    });
  }

  deleteProjectRole(projectRole: ProjectRoleDto) {
    this.projectRoleService.delete(projectRole.id).subscribe({
      next: (res) => {
        this.notificationService.showSuccess(this.localizationService.instant("Work::Message:Delete.Ok", projectRole.name));
        this.getProjectRoles();
      },
      error: (err) => {
        this.notificationService.showError(this.localizationService.instant("Work::Message:Delete.NotOk", projectRole.name));
      }
    })
  }

  closeProjectRole(event) {
    if (event) {
      this.getProjectRoles();
      this.displayProjectRole = false;
    } else {
      this.displayProjectRole = false;
    }
  }

  mediumSide: boolean = window.innerWidth < 600 ? false : true;

  smallSide: boolean = window.innerWidth < 600 ? true : false;

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
