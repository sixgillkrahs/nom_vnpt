import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CsTieredMenuComponent } from './cs-tieredMenu.component';

import { TieredMenuModule } from 'primeng/tieredmenu';
import { ButtonModule } from 'primeng/button';

@NgModule({
  imports: [
    CommonModule,
    TieredMenuModule,
    ButtonModule
  ],
  declarations: [CsTieredMenuComponent],
  exports: [CsTieredMenuComponent]
})
export class CsTieredMenuModule { }
