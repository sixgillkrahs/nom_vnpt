import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LocalizationService, NotificationService, UtilityService } from '@ctincsp/ng.core';
import { OrganizationUnitService } from '@ctincsp/ng.identity/proxy';
import { ProjectGeneralService } from '@proxy/project-generals';
import { ProjectStateService } from '@proxy/project-state';
import { GetAllProjectStateDto } from '@proxy/project-state/dtos';
import { ProjectDetailData } from 'src/app/shared/ProjectManager/models/project-manager.model';

@Component({
  selector: 'app-project-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit {

  @Input() displayProjectDetail: boolean = false;
  @Input() projectDetailData: ProjectDetailData;
  @Output() closeProjectDetail = new EventEmitter();

  isLoading: boolean = false;

  formGroup: FormGroup;

  departments: any[] = [];

  stateOption: any[] = [];

  constructor(
    private fb: FormBuilder,
    private util: UtilityService,
    private projectGeneralService: ProjectGeneralService,
    private notificationService: NotificationService,
    private localizationService: LocalizationService,
    private organizationUnitService: OrganizationUnitService,
    private projectStateService: ProjectStateService
  ) { }

  get f() { return this.formGroup.controls; }

  ngOnInit() {
    this.createFormBuild();
    this.getProject();
    this.getOrganizationUnitService();
    this.getProjectStateDropdown();
  }

  getProject(){
    if(this.projectDetailData.id) {
      this.isLoading = true;
      this.projectGeneralService.get(this.projectDetailData.id).subscribe({
        next: (res) => {
          this.isLoading = false;
          this.formGroup.patchValue(res)
          console.log(res);
        },
        error: (err) => {
          this.isLoading = false;
        }
      })
    }
  }

  getOrganizationUnitService(){
    this.isLoading = true;
    this.organizationUnitService.getList({
      filter: null,
      maxResultCount: null,
      skipCount: null,
    }).subscribe({
      next: (res) => {
        this.isLoading = false;
        this.departments = res.items;
        console.log(res);
      },
      error: (err) => {
        this.isLoading = false;
      }
    })
  }

  getProjectStateDropdown(){
    let filterState: GetAllProjectStateDto = {
      filter: null,
      sorting: null
    }
    this.isLoading = true;
    this.projectStateService.getDropdown(filterState).subscribe({
      next: (res) => {
        this.stateOption = res;
        this.isLoading = false;
      },
      error: (err) => {
        this.isLoading = false;
      }
    })
  }

  createFormBuild(){
    this.formGroup = this.fb.group({
      code: [null, [Validators.required, this.validatorCode()]],
      name: [null, [Validators.required, this.validatorName()]],
      departmentID: [null, Validators.required],
      projectStateID: [null],
      description: [null],
      status: true
    });
  }

  validatorCode() {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const regex = /^[a-zA-Z0-9]*$/;
      const valid = regex.test(control.value);
      const messageTxt = this.localizationService.instant("Work::Pm:Display.Project.Code");
      return valid ? null : { message: this.localizationService.instant("Work::Message:IsNotMatch", messageTxt)};
    };
  }

  validatorName() {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const regex = /^[^!@#$%^&*()_+\=\-\[\]\{\}\|:;~`'"\./<>?]+$/;
      const valid = regex.test(control.value);
      const messageTxt = this.localizationService.instant("Work::Pm:Display.Project.Name");
      return valid ? null : { message: this.localizationService.instant("Work::Message:IsNotMatch", messageTxt)};
    };
  }

  save(){
    this.util.markAllControlsAsDirty([this.formGroup]);
    if (this.formGroup.invalid) {
      return;
    }
    this.isLoading = true;

    let createUpdate;
    if(this.projectDetailData.id){
      createUpdate = this.projectGeneralService.updateRoot(this.projectDetailData.id,this.formGroup.value);
    }else{
      createUpdate=  this.projectGeneralService.create(this.formGroup.value);
    }

    createUpdate.subscribe({
      next: (res) => {
        this.isLoading = false;
        this.projectDetailData.id ?
        this.notificationService.showSuccess(this.localizationService.instant("Work::Message:Update.Ok")) :
        this.notificationService.showSuccess(this.localizationService.instant("Work::Message:Create.Ok"));
        this.closeProjectDetail.emit(true);
      },
      error: (err) => {
        this.isLoading = false;
        this.projectDetailData.id ?
        this.notificationService.showError(this.localizationService.instant("Work::Message:Update.NotOk")) :
        this.notificationService.showError(this.localizationService.instant("Work::Message:Create.NotOk"));
      }
    })

  }
}
