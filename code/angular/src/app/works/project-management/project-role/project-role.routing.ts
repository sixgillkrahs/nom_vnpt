import { Routes, RouterModule } from '@angular/router';
import { ProjectRoleComponent } from './project-role.component';

const routes: Routes = [
  { path: "", component: ProjectRoleComponent },
];

export const ProjectRoleRoutes = RouterModule.forChild(routes);
