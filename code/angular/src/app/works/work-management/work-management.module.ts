import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WorkManagementComponent } from './work-management.component';
import { WorkManagementRoutes } from './work-management.routing';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { SharedModule } from 'src/app/shared/shared.module';
import { PrimeShareModule } from '@ctincsp/ng.core';
import { RenderErrorModule2 } from 'src/app/shared/components/render-error/render-error.module';
import { CsTieredMenuModule } from 'src/app/shared/components/cs-tieredMenu/cs-tieredMenu.module';
import { WorkListComponent } from './work-list/work-list.component';
import { WorkAddComponent } from './add/work-add.component';
import { WorkHandOverComponent } from './hand-over/work-hand-over.component';
import { IdentityModule } from '@ctincsp/ng.identity';
import { WorkCollaboratorComponent } from './collaborator/work-collaborator.component';
import { WorkViewMasterComponent } from './master-view/work-master-view.component';
import { TabViewModule } from 'primeng/tabview';

const WORK_LIST_COMPONENTS = [
  WorkListComponent,
  WorkAddComponent,
  WorkHandOverComponent,
  WorkCollaboratorComponent,
  WorkViewMasterComponent
]

@NgModule({
  imports: [
    CommonModule,
    BreadcrumbModule,
    IdentityModule,
    SharedModule,
    PrimeShareModule,
    RenderErrorModule2,
    CsTieredMenuModule,
    TabViewModule,

    WorkManagementRoutes
  ],
  declarations: [WorkManagementComponent, ...WORK_LIST_COMPONENTS],
  exports: [WorkListComponent]
})
export class WorkManagementModule { }
