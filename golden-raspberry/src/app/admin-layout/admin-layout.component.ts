// import { Component } from '@angular/core';

// @Component({
//   selector: 'app-admin-layout',
//   imports: [],
//   templateUrl: './admin-layout.component.html',
//   styleUrl: './admin-layout.component.scss'
// })
// export class AdminLayoutComponent {

// }


import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-admin-layout',
  standalone: true,
  imports: [RouterModule],
  template: `
    <div class="d-flex">
      <!-- Sidebar -->
      <div class="sidebar bg-dark text-white">
        <h4 class="text-center py-3">Admin Panel</h4>
        <a routerLink="/dashboard" routerLinkActive="active">Dashboard</a>
        <a routerLink="/list" routerLinkActive="active">List</a>
      </div>

      <!-- Content -->
      <div class="content flex-grow-1 p-4">
        <router-outlet></router-outlet>
      </div>
    </div>
  `,
  styles: [
    `
      .sidebar {
        width: 250px;
        height: 100vh;
        position: fixed;
      }
      .sidebar a {
        color: #fff;
        text-decoration: none;
        padding: 15px;
        display: block;
      }
      .sidebar a.active,
      .sidebar a:hover {
        background: #495057;
      }
      .content {
        margin-left: 250px;
      }
    `,
  ],
})
export class AdminLayoutComponent {}
