import { NgModule } from '@angular/core';
import { ProjectStateComponent } from './project-state.component';
import { ProjectStateRoutes } from './project-state.routing';
import { PrimeShareModule } from '@ctincsp/ng.core';
import { SharedModule } from 'src/app/shared/shared.module';
import { CrudProjectStateComponent } from './crud-projectState/crud-projectState.component';
import { RenderErrorModule2 } from 'src/app/shared/components/render-error/render-error.module';
import { CsTieredMenuModule } from 'src/app/shared/components/cs-tieredMenu/cs-tieredMenu.module';

@NgModule({
  imports: [
    SharedModule,
    PrimeShareModule,
    RenderErrorModule2,
    CsTieredMenuModule,
    ProjectStateRoutes
  ],
  declarations: [ProjectStateComponent, CrudProjectStateComponent]
})
export class ProjectStateModule { }
