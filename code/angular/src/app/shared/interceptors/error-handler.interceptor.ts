import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { LocalizationService, NotificationService } from '@ctincsp/ng.core';

@Injectable()
export class GlobalHttpInterceptorService implements HttpInterceptor {
  constructor(
    private notificationService: NotificationService,
    private localizationService: LocalizationService
  ) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(ex => {
        let InternalServerErrorMessage = '';
        if (ex.error?.error?.message) {
          this.notificationService.showError(ex.error.error.message);
        } else {
          switch (ex.status) {
            case 401:
              InternalServerErrorMessage = this.localizationService.instant(
                'AbpIdentity::DefaultErrorMessage401Detail'
              );
              this.notificationService.showError(InternalServerErrorMessage);
              break;
            case 403:
              InternalServerErrorMessage = this.localizationService.instant(
                'AbpIdentity::DefaultErrorMessage403Detail'
              );
              this.notificationService.showError(InternalServerErrorMessage);
              break;
            case 404:
              InternalServerErrorMessage = this.localizationService.instant(
                'AbpIdentity::DefaultErrorMessage404Detail'
              );
              this.notificationService.showError(InternalServerErrorMessage);
              break;
            case 500:
              InternalServerErrorMessage = this.localizationService.instant(
                'AbpIdentity::InternalServerErrorMessage'
              );
              this.notificationService.showError(InternalServerErrorMessage);
              break;
            default:
              InternalServerErrorMessage = this.localizationService.instant(
                'AbpIdentity::InternalServerErrorMessage'
              );
              this.notificationService.showError(InternalServerErrorMessage);
              break;
          }
        }
        console.error("CSP ERR: ", ex?.error);
        return throwError(ex);
      })
    );
  }
}
