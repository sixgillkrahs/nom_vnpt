import { Routes, RouterModule } from '@angular/router';
import { ProjectManagementComponent } from './project-management.component';

const routes: Routes = [
    { path: '', component: ProjectManagementComponent, data: { name: 'Work::ProjectManagement' },
      children: [
        {
          path: 'department',
          data: { name: 'Work::Pm:Department' },
          loadChildren: () => import('./department/department.module').then(m => m.DepartmentModule)
        },
        {
          path: 'projects',
          data: { name: 'Work::Pm:Project' },
          loadChildren: () => import('./project/project.module').then(m => m.ProjectModule)
        },
        {
          path: 'project-role',
          data: { name: 'Work::Pm:ProjectRole' },
          loadChildren: () => import('./project-role/project-role.module').then(m => m.ProjectRoleModule)
        },
        {
          path: 'project-state',
          data: { name: 'Work::Pm:ProjectState' },
          loadChildren: () => import('./project-state/project-state.module').then(m => m.ProjectStateModule)
        },
    ]},
];

export const ProjectManagementRoutes = RouterModule.forChild(routes);
