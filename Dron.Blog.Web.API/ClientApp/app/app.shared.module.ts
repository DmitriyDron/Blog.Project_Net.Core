import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule, RequestOptions, Http } from '@angular/http';
import { RouterModule } from '@angular/router';
import { TagInputModule } from 'ngx-chips';
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { ToastyModule } from 'ng2-toasty';
import { CategoriesComponent } from './components/categories/categories.component';
import { BlogComponent } from './components/blog/blog.component';
import { PostviewComponent } from './components/post-view/post-view.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { PostformComponent } from './components/post-form/post-form.component';
import { PostsComponent } from './components/posts/posts.component';
import { PostService } from './services/post.service';
import { CategoryService } from './services/category.service';
import { TagService } from './services/tag.service';
import { GlobalEventsService } from './services/global-events.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { LoginComponent } from './components/account/login/login.component';
import { FooterComponent } from './layout/footer/footer.component';
import { AuthHttp, AuthConfig, JwtHelper } from 'angular2-jwt';
import { FlashMessagesModule } from 'angular2-flash-messages';
import { HttpService } from './services/auth/http.service';
import { AuthGuard } from './services/auth/auth.guard';
import { RegisterComponent } from './components/account/register/register.component';
import { HeaderComponent } from './layout/header/header.component';



@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        PostsComponent,
        PostformComponent,
        PaginationComponent,
        PostviewComponent,
        BlogComponent,
        CounterComponent,
        CategoriesComponent,
        HomeComponent,
        LoginComponent,
        FooterComponent,
        RegisterComponent,
        HeaderComponent,
        HomeComponent




    ],
    imports: [
        CommonModule,
        HttpModule,
        HttpClientModule,
        FormsModule,
        BrowserModule,
        TagInputModule,
        ReactiveFormsModule,
        ToastyModule.forRoot(),
        FlashMessagesModule.forRoot(),
        RouterModule.forRoot([
        
          { path: '', component: BlogComponent, pathMatch: 'full' },
          { path: 'tag/:id', component: BlogComponent },
          { path: 'posts', component: PostsComponent, canActivate: [AuthGuard] },
          { path: 'posts/categories', component: CategoriesComponent, canActivate: [AuthGuard] },
          { path: 'login', component: LoginComponent },
          { path: 'register', component: RegisterComponent },
          { path: 'post/create', component: PostformComponent,canActivate: [AuthGuard] },
          { path: 'post/:id', component: PostviewComponent },
          { path: 'post/edit/:id', component: PostformComponent, canActivate: [AuthGuard]},
          { path: 'home', redirectTo: '' },
          { path: '**', redirectTo: '' }
          
         
      ]),
      
      ],
    providers: [PostService, CategoryService, TagService, GlobalEventsService, HttpService
  ]
})
export class AppModuleShared {
}