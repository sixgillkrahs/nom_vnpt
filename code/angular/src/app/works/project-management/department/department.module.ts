import { NgModule } from '@angular/core';
import { DepartmentComponent } from './department.component';
import { DepartmentRoutes } from './department.routing';
import { PrimeShareModule } from '@ctincsp/ng.core';
import { TreeSelectModule } from 'primeng/treeselect';
import { AddDepartmentComponent } from './add-department/add-department.component';
import { TabViewModule } from 'primeng/tabview';
import { ProgressBarModule } from 'primeng/progressbar';
import { MembersComponent } from './department-container/members/members.component';
import { ProcessComponent } from './department-container/process/process.component';
import { DepartmentDetailComponent } from './department-container/department-detail/department-detail.component';
import { TeamsComponent } from './department-container/teams/teams.component';
import { WorkTemplateComponent } from './department-container/work-template/work-template.component';
import { WorksComponent } from './department-container/works/works.component';
import { DepartmentContainerComponent } from './department-container/department-container.component';
import { SharedModule } from 'src/app/shared/shared.module';

const DETAIL_DEPARTMENT_COMPONENT = [
  MembersComponent,
  ProcessComponent,
  DepartmentDetailComponent,
  TeamsComponent,
  WorkTemplateComponent,
  WorksComponent,
]

const _PRIME_MODULE = [
  TreeSelectModule,
  TabViewModule,
  ProgressBarModule,
]


@NgModule({
  imports: [
    SharedModule,
    PrimeShareModule,
    ..._PRIME_MODULE,
    DepartmentRoutes
  ],
  declarations: [DepartmentComponent, AddDepartmentComponent, DepartmentContainerComponent , ...DETAIL_DEPARTMENT_COMPONENT]
})
export class DepartmentModule { }
