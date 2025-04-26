import { AccountConfigModule } from '@ctincsp/ng.account/config';
import { CoreModule, NotificationService, UtilityService } from '@ctincsp/ng.core';
import { registerLocale } from '@ctincsp/ng.core/locale';
import { IdentityConfigModule } from '@ctincsp/ng.identity/config';
import { SettingManagementConfigModule } from '@ctincsp/ng.setting-management/config';
import { TenantManagementConfigModule } from '@ctincsp/ng.tenant-management/config';
import { ThemeSharedModule } from '@ctincsp/ng.theme.shared';
import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { environment } from '../environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { APP_ROUTE_PROVIDER } from './route.provider';

import { AbpOAuthModule } from '@ctincsp/ng.oauth';
import { ServiceWorkerModule } from '@angular/service-worker';
import { CommonModule } from '@angular/common';
import { ToastModule } from 'primeng/toast';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './shared/interceptors/token.interceptor';
import { GlobalHttpInterceptorService } from './shared/interceptors/error-handler.interceptor';
import { DialogService } from 'primeng/dynamicdialog';
// import { NotificationService } from './shared/services/notification.service';
import { BlockUIModule } from 'primeng/blockui';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ThemeSakaiModule } from '@ctincsp/ng.theme.sakai';
import { CategoryConfigModule } from '@ctincsp/ng.category/config';
import { MySettingsComponent } from './my-settings/my-settings.component';
import { NotificationConfigConfigModule } from '@ctincsp/ng.notification-config/config';
import { RouteModulePaths } from './route-module-path';

import { storeLocaleData } from '@ctincsp/ng.core/locale';
import(`@/../@angular/common/locales/vi.mjs`).then(m => storeLocaleData(m.default, 'vi'));
import localeVi from '@angular/common/locales/vi';

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    CoreModule.forRoot({
      environment,
      registerLocaleFn: registerLocale(),
    }),

    AbpOAuthModule.forRoot(),
    ThemeSharedModule.forRoot(),
    AccountConfigModule.forRoot('account'),
    IdentityConfigModule.forRoot('identity'),
    TenantManagementConfigModule.forRoot('tenant-management'),
    SettingManagementConfigModule.forRoot('setting-management'),
    NotificationConfigConfigModule.forRoot(RouteModulePaths.NotificationConfig),
    CategoryConfigModule.forRoot('category'),
    // FeatureManagementModule.forRoot(),

    ToastModule,
    ConfirmDialogModule,
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: environment.production,
      // Register the ServiceWorker as soon as the application is stable
      // or after 30 seconds (whichever comes first).
      registrationStrategy: 'registerWhenStable:30000'
    }),
    BlockUIModule,
    ProgressSpinnerModule,
    AppRoutingModule,
    ThemeSakaiModule.forRoot()
  ],
  providers: [
    APP_ROUTE_PROVIDER,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: GlobalHttpInterceptorService,
      multi: true,
    },
    { provide: LOCALE_ID, useValue: 'vi' },
    DialogService,
    NotificationService,
    ConfirmationService,
    UtilityService,
  ],
  declarations: [AppComponent, MySettingsComponent],
  bootstrap: [AppComponent],
})
export class AppModule { }
