import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuComponent } from './menu.component';
import { MenuRoutingModule } from './menu-routing.module';

import { TreeTableModule } from 'primeng/treetable';
import { ButtonModule } from 'primeng/button';
import { AddMenuComponent } from './add-menu/add-menu.component';
import { DialogModule } from 'primeng/dialog';
import { DividerModule } from 'primeng/divider';
import { InputTextModule } from 'primeng/inputtext';
import { RippleModule } from 'primeng/ripple';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { InputNumberModule } from 'primeng/inputnumber';
import { CheckboxModule } from 'primeng/checkbox';
import { TreeSelectModule } from 'primeng/treeselect';
import { SelectButtonModule } from 'primeng/selectbutton';
import { DropdownModule } from 'primeng/dropdown';
import { ConfirmationService, MessageService } from 'primeng/api';
import { UpdateMenuComponent } from './update-menu/update-menu.component';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { MenuService } from '@proxy/menu';
import { PrimeShareModule } from '@ctincsp/ng.core';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    MenuRoutingModule,
    TreeTableModule,
    ButtonModule,
    RippleModule,
    InputTextModule,
    DividerModule,
    DialogModule,
    FormsModule,
    ReactiveFormsModule,
    AutoCompleteModule,
    InputNumberModule,
    CheckboxModule,
    ButtonModule,
    SelectButtonModule,
    TreeSelectModule,
    DropdownModule,
    ToastModule,
    ConfirmDialogModule,
    SharedModule,
    PrimeShareModule
  ],
  declarations: [
    MenuComponent,
    AddMenuComponent,
    UpdateMenuComponent
  ],
  providers: [MenuService, MessageService, ConfirmationService]
})
export class MenuModule { }
