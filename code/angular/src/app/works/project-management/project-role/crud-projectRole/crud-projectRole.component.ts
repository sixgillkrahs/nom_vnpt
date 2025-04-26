import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LocalizationService, NotificationService, UtilityService } from '@ctincsp/ng.core';
import { ProjectRoleService } from '@proxy/project-role';
import { ProjectRoleData } from 'src/app/shared/ProjectManager/models/project-manager.model';

@Component({
  selector: 'app-crud-projectRole',
  templateUrl: './crud-projectRole.component.html',
  styleUrls: ['./crud-projectRole.component.scss']
})
export class CrudProjectRoleComponent implements OnInit {

  @Input() displayProjectRole: boolean = false;
  @Input() projectRoleData: ProjectRoleData;
  @Output() closeProjectRole = new EventEmitter();

  formGroup: FormGroup;

  isLoading: boolean = false;

  constructor(
    private fb: FormBuilder,
    private util: UtilityService,
    private projectRoleService: ProjectRoleService,
    private notificationService: NotificationService,
    private localizationService: LocalizationService,
  ) { }

  get f() { return this.formGroup.controls; }

  ngOnInit() {
    this.createFormBuilder();
  }

  createFormBuilder(){
    this.formGroup = this.fb.group({
      code: [null, [Validators.required, this.validatorCode()]],
      name: [null, [Validators.required, this.validatorName()]],
      domain: [null, Validators.required],
      status: ["true"]
    })

    console.log(this.projectRoleData);
    if(this.projectRoleData.id){
      this.formGroup.patchValue(this.projectRoleData.param)
    }
  }

  validatorCode() {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const regex = /^[a-zA-Z0-9]*$/;
      const valid = regex.test(control.value);
      const messageTxt = this.localizationService.instant("Work::Pm:ProjectRole.Code");
      return valid ? null : { message: this.localizationService.instant("Work::Message:IsNotMatch", messageTxt)};
    };
  }

  validatorName() {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const regex = /^[^!@#$%^&*()_+\=\-\[\]\{\}\|:;~`'"\./<>?]+$/;
      const valid = regex.test(control.value);
      const messageTxt = this.localizationService.instant("Work::Pm:ProjectRole.Name");
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

    if(this.projectRoleData.id){
      createUpdate = this.projectRoleService.updateRoot(this.projectRoleData.id, this.formGroup.value);
    }else{
      createUpdate = this.projectRoleService.create(this.formGroup.value);
    }

    createUpdate.subscribe({
      next: (res) => {
        this.isLoading = false;
        this.closeProjectRole.emit(true);

        console.log(this.formGroup.value);

        if(this.projectRoleData.id){
          this.notificationService.showSuccess(this.localizationService.instant("Work::Message:Update.Ok"));
        }else{
          this.notificationService.showSuccess(this.localizationService.instant("Work::Message:Create.Ok"));
        }

        console.log(res);

      },
      error: (err) => {
        this.isLoading = false;

        if(this.projectRoleData.id){
          this.notificationService.showError(this.localizationService.instant("Work::Message:Update.NotOk"));
        }else{
          this.notificationService.showError(this.localizationService.instant("Work::Message:Create.NotOk"));
        }
      }
    });

  }

}
