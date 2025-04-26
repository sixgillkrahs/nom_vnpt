import { Routes, RouterModule } from '@angular/router';
import { DepartmentComponent } from './department.component';

const routes: Routes = [
  { path: '', component: DepartmentComponent },
];

export const DepartmentRoutes = RouterModule.forChild(routes);
