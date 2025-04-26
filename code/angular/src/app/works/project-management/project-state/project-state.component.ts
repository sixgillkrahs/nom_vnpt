import { Component, HostListener, OnInit } from '@angular/core';
import { LocalizationService, NotificationService } from '@ctincsp/ng.core';
import { ProjectStateService } from '@proxy/project-state';
import { ProjectStateDto, ProjectStateGetListInput } from '@proxy/project-state/dtos';
import { ConfirmationService } from 'primeng/api';
import { ProjectStateData } from 'src/app/shared/ProjectManager/models/project-manager.model';

@Component({
  selector: 'app-project-state',
  templateUrl: './project-state.component.html',
  styleUrls: ['./project-state.component.scss']
})
export class ProjectStateComponent implements OnInit {

  isLoading: boolean = false;

  states: any[] = [];

  projectStateFilter: ProjectStateGetListInput;

  selectedState: any[] = [];

  selectedProjectState!: any[] | null;

  selectedCity: any;

  cities: any[];

  displayProjectState: boolean = false;

  projectStateData: ProjectStateData;

  keyword: any;
  public totalCount: number;
  skipCount = 0;
  maxResultCount = 10;

  constructor(
    private projectStateService: ProjectStateService,
    private confirmationService: ConfirmationService,
    private localizationService: LocalizationService,
    private notificationService: NotificationService
  ) { }

  ngOnInit() {
    this.getProjectStates();

     this.cities = [
      { name: 'New York', code: 'NY' },
      { name: 'Rome', code: 'RM' },
      { name: 'London', code: 'LDN' },
      { name: 'Istanbul', code: 'IST' },
      { name: 'Paris', code: 'PRS' }
    ];

  }

  actionItems: any[] = [];
  dropdownItemsButton(projectState){
    this.actionItems = [
      {
          label: this.localizationService.instant("Work::Edit"),
          icon: 'pi pi-pencil',
          command: () => { this.showModal(projectState)}
      },
      { separator: true},
      {
          label: this.localizationService.instant("Work::Delete"),
          icon: "pi pi-trash",
          command: () => { this.confirmDelete(projectState); },
      },
    ]
  }

  getProjectStates(){
    this.isLoading = true;
    this.projectStateFilter = {
      skipCount: this.skipCount,
      maxResultCount: this.maxResultCount,
      filter: this.keyword
    }
    this.projectStateService.getList(this.projectStateFilter).subscribe({
      next: (res) => {
        this.states = res.items;
        this.isLoading = false;
        this.totalCount = res.totalCount;
      },
      error: (err) => {
        this.isLoading = false;
      }
    })
  }

  onPageChange(event){
    this.skipCount = event.page * this.maxResultCount;
    this.maxResultCount = event.rows;
    this.getProjectStates();
  }

  showModal(projectState?:any){
    this.displayProjectState = true;

    let data: ProjectStateData = {
      header: this.localizationService.instant('Work::Pm:ProjectState.New')
    }

    if(projectState){
      data.id = projectState.id;
      data.param = projectState;
    }

    this.projectStateData = data;
  }

  confirmDelete(projectState:ProjectStateDto){
    this.confirmationService.confirm({
      message: this.localizationService.instant('Work::Message:Confirm.Delete', projectState.name ),
      accept: () => {
        this.deleteProjectRole(projectState);
      },
    });
  }

  deleteProjectRole(projectState:ProjectStateDto){
    this.projectStateService.delete(projectState.id).subscribe({
      next: (res) => {
        this.notificationService.showSuccess(this.localizationService.instant("Work::Message:Delete.Ok", projectState.name));
        this.getProjectStates();
      },
      error: (err) => {
        this.notificationService.showError(this.localizationService.instant("Work::Message:Delete.NotOk", projectState.name));
      }
    })
  }

  closeProjectState(event){
    if(event){
      this.getProjectStates();
      this.displayProjectState = false;
    }else{
      this.displayProjectState = false;
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
