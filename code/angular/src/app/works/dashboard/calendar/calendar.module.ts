import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CalendarComponent } from './calendar.component';
import { CalendarRoutes } from './calendar.routing';

@NgModule({
  imports: [
    CommonModule,
    CalendarRoutes
  ],
  declarations: [CalendarComponent]
})
export class CalendarModule { }
