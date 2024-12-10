import { Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MoviesListComponent } from './movies-list/movies-list.component';
import { AdminLayoutComponent } from './admin-layout/admin-layout.component';



export const routes: Routes = [
  {
    path: '',
    component: AdminLayoutComponent,
    children: [
      { path: 'dashboard', component: DashboardComponent },
      { path: 'list', component: MoviesListComponent },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    ],
  },
];
