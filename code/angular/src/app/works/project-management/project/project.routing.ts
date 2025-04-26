import { Routes, RouterModule } from '@angular/router';
import { ProjectComponent } from './project.component';

const routes: Routes = [
  { path: '', component: ProjectComponent },
];

export const ProjectRoutes = RouterModule.forChild(routes);
