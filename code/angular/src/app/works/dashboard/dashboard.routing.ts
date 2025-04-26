import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard.component';

const routes: Routes = [
  { path: '', component: DashboardComponent , children: [
    { path: 'calendar',
      data: {name: "Lịch"},
      loadChildren: () => import('./calendar/calendar.module').then(m => m.CalendarModule)
    }
  ]},
];

export const DashboardRoutes = RouterModule.forChild(routes);
