import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatBadgeModule } from '@angular/material/badge';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { MatChipsModule } from '@angular/material/chips';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

@NgModule({
  imports: [
     CommonModule,
     MatButtonModule,
     MatToolbarModule,
     MatIconModule,
     MatSidenavModule,
     MatBadgeModule,
     MatListModule,
     MatGridListModule,
     MatFormFieldModule,
     MatInputModule,
     MatSelectModule,
     MatRadioModule,
     MatChipsModule,
     MatTooltipModule,
     MatTableModule,
     MatPaginatorModule,
     MatCardModule,
     MatMenuModule,
     MatSlideToggleModule
  ],
  exports: [
     MatButtonModule,
     MatToolbarModule,
     MatIconModule,
     MatSidenavModule,
     MatBadgeModule,
     MatListModule,
     MatGridListModule,
     MatInputModule,
     MatFormFieldModule,
     MatSelectModule,
     MatRadioModule,
     MatChipsModule,
     MatTooltipModule,
     MatTableModule,
     MatPaginatorModule,
     MatCardModule,
     MatMenuModule
  ],
})

export class AngularMaterialModule { }
