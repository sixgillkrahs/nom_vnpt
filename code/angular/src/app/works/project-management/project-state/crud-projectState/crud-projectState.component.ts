import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LocalizationService, NotificationService, UtilityService } from '@ctincsp/ng.core';
import { ProjectStateService } from '@proxy/project-state';
import { ProjectStateData } from 'src/app/shared/ProjectManager/models/project-manager.model';

@Component({
  selector: 'app-crud-projectState',
  templateUrl: './crud-projectState.component.html',
  styleUrls: ['./crud-projectState.component.scss']
})
export class CrudProjectStateComponent implements OnInit {

  @Input() displayProjectState: boolean = false;
  @Input() projectStateData: ProjectStateData;
  @Output() closeProjectState = new EventEmitter();

  formGroup: FormGroup;

  isLoading: boolean = false;

  constructor(
    private fb: FormBuilder,
    private util: UtilityService,
    private projectStateService: ProjectStateService,
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
    })

    console.log(this.projectStateData);
    if(this.projectStateData.id){
      this.formGroup.patchValue(this.projectStateData.param)
    }
  }

  validatorCode() {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const regex = /^[a-zA-Z0-9]*$/;
      const valid = regex.test(control.value);
      const messageTxt = this.localizationService.instant("Work::Pm:ProjectState.Code");
      return valid ? null : { message: this.localizationService.instant("Work::Message:IsNotMatch", messageTxt)};
    };
  }

  validatorName() {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const regex = /^[^!@#$%^&*()_+\=\-\[\]\{\}\|:;~`'"\./<>?]+$/;
      const valid = regex.test(control.value);
      const messageTxt = this.localizationService.instant("Work::Pm:ProjectState.Name");
      return valid ? null : { message: this.localizationService.instant("Work::Message:IsNotMatch", messageTxt)};
    };
  }

  save(){
    this.util.markAllControlsAsDirty([this.formGroup]);
    console.log(this.formGroup);
    if (this.formGroup.invalid) {
      return;
    }

    this.isLoading = true;

    let createUpdate;

    if(this.projectStateData.id){
      createUpdate = this.projectStateService.updateRoot(this.projectStateData.id, this.formGroup.value);
    }else{
      createUpdate = this.projectStateService.create(this.formGroup.value);
    }

    createUpdate.subscribe({
      next: (res) => {
        this.isLoading = false;
        this.closeProjectState.emit(true);

        if(this.projectStateData.id){
          this.notificationService.showSuccess(this.localizationService.instant("Work::Message:Update.Ok"));
        }else{
          this.notificationService.showSuccess(this.localizationService.instant("Work::Message:Create.Ok"));
        }

      },
      error: (err) => {
        this.isLoading = false;

        if(this.projectStateData.id){
          this.notificationService.showError(this.localizationService.instant("Work::Message:Update.NotOk"));
        }else{
          this.notificationService.showError(this.localizationService.instant("Work::Message:Create.NotOk"));
        }
      }
    });

  }

}
