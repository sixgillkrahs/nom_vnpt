import { Routes, RouterModule } from '@angular/router';
import { ProjectStateComponent } from './project-state.component';

const routes: Routes = [
  { path: "", component: ProjectStateComponent },
];

export const ProjectStateRoutes = RouterModule.forChild(routes);
