import { Routes, RouterModule } from '@angular/router';
import { ResourseManagementComponent } from './resourse-management.component';

const routes: Routes = [
    {path: '', component: ResourseManagementComponent,data: { name: 'Quản lý tài nguyên' },
    children: [
            {
                path: 'test',
                data: { name: "Thử" },
                loadChildren: () => import('./test/test.module').then(m => m.TestModule)
            }
        ]
    },
];

export const ResourseManagementRoutes = RouterModule.forChild(routes);
