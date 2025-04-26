import { NgModule } from '@angular/core';
import { ProjectRoleComponent } from './project-role.component';
import { ProjectRoleRoutes } from './project-role.routing';
import { PrimeShareModule } from '@ctincsp/ng.core';
import { SharedModule } from 'src/app/shared/shared.module';
import { CrudProjectRoleComponent } from './crud-projectRole/crud-projectRole.component';
import { CsTieredMenuModule } from 'src/app/shared/components/cs-tieredMenu/cs-tieredMenu.module';
import { RenderErrorModule2 } from 'src/app/shared/components/render-error/render-error.module';

@NgModule({
  imports: [
    SharedModule,
    PrimeShareModule,
    CsTieredMenuModule,
    RenderErrorModule2,
    ProjectRoleRoutes
  ],
  declarations: [ProjectRoleComponent,CrudProjectRoleComponent]
})
export class ProjectRoleModule { }
