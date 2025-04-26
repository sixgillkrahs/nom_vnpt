import { Routes, RouterModule } from '@angular/router';
import { DepartmentTeamComponent } from './department-team.component';

const routes: Routes = [
  { path: "",
    data: { name: 'Work::DepartmentTeam' },
    component: DepartmentTeamComponent },
];

export const DepartmentTeamRoutes = RouterModule.forChild(routes);
