import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Student } from 'src/app/interface/models';
import { StudentServiceService } from 'src/app/service/StudentService/student-service.service';

@Component({
  selector: 'app-student-table',
  templateUrl: './student-table.component.html',
  styleUrls: ['./student-table.component.css']
})
export class StudentTableComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['code','name','actions'];
  dataSource = new MatTableDataSource<Student>(); // cambiar aquí

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(public _StudentService: StudentServiceService) { }

  ngOnInit() {
    this._StudentService.getAllStudent().subscribe({
      next: (data) => {
        this.dataSource.data = data;
        console.log(data[0]);
      },
      error: (err) => {
        console.log("error en la petición http");
      },
    });
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
}


