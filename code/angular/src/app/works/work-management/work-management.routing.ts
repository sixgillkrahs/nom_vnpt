import { Routes, RouterModule } from '@angular/router';
import { WorkManagementComponent } from './work-management.component';
import { WorkListComponent } from './work-list/work-list.component';

const routes: Routes = [
  { path: "", component: WorkManagementComponent,
    data: { name: 'Work::WorkManagement' },
    children: [
      {
        path: "work-list",
        data: { name: 'Work::WorkList' },
        component: WorkListComponent
      }
    ]},
];

export const WorkManagementRoutes = RouterModule.forChild(routes);
