import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RenderErrorComponent } from './render-error.component';
import { TagModule } from 'primeng/tag';

@NgModule({
  imports: [
    CommonModule,
    TagModule
  ],
  declarations: [RenderErrorComponent],
  exports: [RenderErrorComponent]
})
export class RenderErrorModule2 { }
