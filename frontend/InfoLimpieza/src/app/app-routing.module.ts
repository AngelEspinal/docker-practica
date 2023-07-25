import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '',
   redirectTo: 'home',
   pathMatch: 'full'
   },
  {
    path: 'home',
    loadChildren: () => import('./module-student/module-student.module').then(m => m.ModuleStudentModule)
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
