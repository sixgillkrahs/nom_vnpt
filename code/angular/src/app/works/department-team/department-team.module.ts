import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DepartmentTeamComponent } from './department-team.component';
import { DepartmentTeamRoutes } from './department-team.routing';
import { PrimeShareModule } from '@ctincsp/ng.core';
import { StateComponent } from './department-team-container/state/state.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { DetailComponent } from './detail/detail.component';
import { DepartmentTeamContainerComponent } from './department-team-container/department-team-container.component';
import { TabViewModule } from 'primeng/tabview';
import { DepartmentTeamWorksComponent } from './department-team-container/department-team-works/department-team-works.component';
import { DepartMentTeamMembersComponent } from './department-team-container/department-team-members/department-team-members.component';
import { TreeSelectModule } from 'primeng/treeselect';
import { DropdownModule } from 'primeng/dropdown';
import { ProgressSpinnerModule } from 'primeng/progressspinner'
import { TreeModule } from 'primeng/tree';
import { ConfigurationApprovalComponent } from './configuration-approval/configuration-approval.component';
import { CsTieredMenuModule } from 'src/app/shared/components/cs-tieredMenu/cs-tieredMenu.module';
const DEPARTMENTTEAM_CONTAINER_SHARE = [
  StateComponent,
  DepartMentTeamMembersComponent,
  DepartmentTeamWorksComponent,

]

@NgModule({
  imports: [
    CommonModule,

    SharedModule,
    PrimeShareModule,
    TabViewModule,

    DepartmentTeamRoutes,
    TreeSelectModule,
    DropdownModule,
    ProgressSpinnerModule,
    TreeModule,
    CsTieredMenuModule
  ],
  declarations: [
    DepartmentTeamComponent,
    DepartmentTeamContainerComponent,
    DetailComponent,
    ConfigurationApprovalComponent,
    ...DEPARTMENTTEAM_CONTAINER_SHARE
  ],
})
export class DepartmentTeamModule { }
