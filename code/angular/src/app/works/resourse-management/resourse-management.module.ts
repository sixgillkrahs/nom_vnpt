import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ResourseManagementComponent } from './resourse-management.component';
import { ResourseManagementRoutes } from './resourse-management.routing';
import { BreadcrumbModule } from 'primeng/breadcrumb';

@NgModule({
    imports: [
        CommonModule,
        BreadcrumbModule,
        ResourseManagementRoutes
    ],
    declarations: [ResourseManagementComponent]
})
export class ResourseManagementModule { }
