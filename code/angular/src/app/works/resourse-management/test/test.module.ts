import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TestRoutingModule } from './test-routing.module';

import { SharedModule } from 'src/app/shared/shared.module';
import { PrimeShareModule } from '@ctincsp/ng.core';
import { CsTieredMenuModule } from 'src/app/shared/components/cs-tieredMenu/cs-tieredMenu.module';
import { RenderErrorModule2 } from 'src/app/shared/components/render-error/render-error.module';
import { TestComponent } from './test.component';



@NgModule({
  imports: [
    SharedModule,
    PrimeShareModule,
    CsTieredMenuModule,
    RenderErrorModule2,
    TestRoutingModule,

  ],
  declarations: [
    TestComponent
  ],
})
export class TestModule { }
