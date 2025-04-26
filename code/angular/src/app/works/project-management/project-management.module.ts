import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectManagementComponent } from './project-management.component';
import { ProjectManagementRoutes } from './project-management.routing';

import { BreadcrumbModule } from 'primeng/breadcrumb';

@NgModule({
  imports: [
    CommonModule,
    BreadcrumbModule,
    ProjectManagementRoutes
  ],
  declarations: [ProjectManagementComponent],
})
export class ProjectManagementModule { }
