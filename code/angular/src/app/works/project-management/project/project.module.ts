import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectComponent } from './project.component';
import { ProjectRoutes } from './project.routing';
import { PrimeShareModule } from '@ctincsp/ng.core';
import { StateComponent } from './project-container/state/state.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { DetailComponent } from './detail/detail.component';
import { ProjectContainerComponent } from './project-container/project-container.component';
import { TabViewModule } from 'primeng/tabview';
import { ProjectWorksComponent } from './project-container/project-works/project-works.component';
import { ProjectMembersComponent } from './project-container/project-members/project-members.component';
import { RenderErrorModule2 } from 'src/app/shared/components/render-error/render-error.module';
import { CsTieredMenuModule } from 'src/app/shared/components/cs-tieredMenu/cs-tieredMenu.module';

const PROJECT_CONTAINER_SHARE = [
  StateComponent,
  ProjectMembersComponent,
  ProjectWorksComponent,

]

@NgModule({
  imports: [
    CommonModule,

    SharedModule,
    PrimeShareModule,
    TabViewModule,
    RenderErrorModule2,
    CsTieredMenuModule,
    ProjectRoutes
  ],
  declarations: [
    ProjectComponent,
    DetailComponent,
    ProjectContainerComponent,
    ...PROJECT_CONTAINER_SHARE
  ],
  providers: []
})
export class ProjectModule { }
