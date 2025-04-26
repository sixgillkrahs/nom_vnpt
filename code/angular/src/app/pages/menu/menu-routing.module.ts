import { RouterModule, Routes } from "@angular/router";
import { MenuComponent } from "./menu.component";
import { NgModule } from "@angular/core";

const routes: Routes = [
  {
    path: '',
    data: {name: 'Quản lí Menu'},
    component: MenuComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MenuRoutingModule { }
