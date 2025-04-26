import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RouteModulePaths } from './route-module-path';

const WORK_ROUTES = [
  {
    path: 'dashboard',
    data: { name: 'Bàn làm việc' },
    loadChildren: () => import('./works/dashboard/dashboard.module').then(m => m.DashboardModule),
  },
  { // Project Management
    path: 'project-management',
    data: { name: 'Work::ProjectManagement' },
    loadChildren: () => import('./works/project-management/project-management.module').then(m => m.ProjectManagementModule),
  },
  { // Department Team
    path: 'department-team',
    data: { name: 'Work::DepartmentTeam' },
    loadChildren: () => import('./works/department-team/department-team.module').then(m => m.DepartmentTeamModule),
  },
  { // Work Management
    path: 'work-management',
    data: { name: 'Work::WorkManagement' },
    loadChildren: () => import('./works/work-management/work-management.module').then(m => m.WorkManagementModule),
  },
  // quan
  { // Resourse Management
    path: 'resourse-management',
    data: { name: 'Quản lý tài nguyên' },
    loadChildren: () => import('./works/resourse-management/resourse-management.module').then(m => m.ResourseManagementModule),
  },
 
]

const routes: Routes = [
  {
    path: '',
    data: { name: 'Trang chủ' },
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
  },
  ...WORK_ROUTES,
  {
    path: 'mail-setting',
    data: { name: 'Quản lí Menu' },
    loadChildren: () => import('./pages/menu/menu.module').then(m => m.MenuModule),
  },
  {
    path: 'menu-management',
    data: { name: 'Quản lí Menu' },
    loadChildren: () => import('./pages/menu/menu.module').then(m => m.MenuModule),
  },
  {
    path: 'category',
    data: { name: 'Quản lý danh mục' },
    loadChildren: () => import('@ctincsp/ng.category').then(m => m.CategoryModule),
  },
  {
    path: RouteModulePaths.NotificationConfig,
    data: { name: 'Quản lý cấu hình thông báo' },
    loadChildren: () => import('@ctincsp/ng.notification-config').then(m => m.NotificationConfigModule),
  },
  {
    path: 'account',
    data: {name: 'Quản lí tài khoản'},
    loadChildren: () => import('@ctincsp/ng.account').then(m => m.AccountModule.forLazy()),
  },
  {
    path: 'identity',
    data: {name: 'Quản lí định danh'},
    loadChildren: () => import('@ctincsp/ng.identity').then(m => m.IdentityModule.forLazy()),
  },
  {
    path: 'tenant-management',
    data: {name: 'Quản lí Tenant'},
    loadChildren: () =>
      import('@ctincsp/ng.tenant-management').then(m => m.TenantManagementModule.forLazy()),
  },
  {
    path: 'setting-management',
    data: {name: 'Quản lí Setting'},
    loadChildren: () =>
      import('@ctincsp/ng.setting-management').then(m => m.SettingManagementModule.forLazy()),
  },
 
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {})],
  exports: [RouterModule],
})
export class AppRoutingModule { }
