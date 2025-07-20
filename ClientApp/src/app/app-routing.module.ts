import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './Shared/components/errors/not-found/not-found.component';
import { PlayComponent } from './play/play.component';
import { AboutComponent } from './about/about.component';
import { AuthorizationGuard } from './Shared/guards/authorization.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthorizationGuard],
    children: [
      { path: 'play', component: PlayComponent },
    ]
  },
  { path: 'about', component: AboutComponent },
  // Implenting lazy loading by the following format  
  { path: 'account', loadChildren: () => import('./account/account.module').then(module => module.AccountModule) },
  { path: 'not-found', component: NotFoundComponent },
  { path: '**', component: NotFoundComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
