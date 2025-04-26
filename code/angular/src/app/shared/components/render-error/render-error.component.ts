import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl } from '@angular/forms';
import { LocalizationService } from '@ctincsp/ng.core';

@Component({
  selector: 'app-render-error',
  templateUrl: './render-error.component.html'
})
export class RenderErrorComponent {

  @Input() entityControl: AbstractControl;
  @Input() keyTranslate: string;
  @Input() validationMessages: any;

  constructor(private localizationService: LocalizationService) {}

  get errorMessage(): string {
    const control = this.entityControl.errors
    if (control) {
      // Xử lý và trả về thông báo lỗi tùy ý dựa trên control.errors
      // Ví dụ:
      if (control.required) {
        return this.localizationService.instant('AbpIdentity::Message:Required');
      }
      if (control.maxlength) {
        return this.localizationService.instant('AbpIdentity::Message:MaxLength', control.maxlength.requiredLength);
      }
      if (control.minlength) {
        return this.localizationService.instant('AbpIdentity::Message:MinLength', control.minlength.requiredLength);
      }
      if (control.max) {
        return this.localizationService.instant('AbpIdentity::Message:MaxValue', control.max.max);
      }
      if (control.min) {
        return this.localizationService.instant('AbpIdentity::Message:MinValue', control.min.min);
      }
      if (control.email) {
        return this.localizationService.instant('AbpIdentity::Message:Invalid.Email');
      }
      if (control.isNotMatch) {
        return this.localizationService.instant('AbpIdentity::Message:Invalid');
      }

      return control.message ?? this.localizationService.instant(control.message);
      // ...
    }

    return null;
  }

}
