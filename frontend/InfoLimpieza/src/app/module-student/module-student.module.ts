import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ModuleStudentRoutingModule } from './module-student-routing.module';
import { StudentComponent } from './student/student.component';
import {MatPaginatorModule} from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { StudentTableComponent } from './student/student-table/student-table.component';
import {MatIconModule} from '@angular/material/icon';
import {MatTooltipModule} from '@angular/material/tooltip';


@NgModule({
  declarations: [
    StudentComponent,
    StudentTableComponent,
  ],
  imports: [
    CommonModule,
    ModuleStudentRoutingModule,
    MatPaginatorModule,
    MatTableModule,
    MatIconModule,
    MatTooltipModule
  ]
})
export class ModuleStudentModule { }
