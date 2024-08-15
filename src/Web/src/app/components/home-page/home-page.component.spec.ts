import { ComponentFixture, TestBed } from "@angular/core/testing";

import { HomePageComponent } from "./home-page.component";
import { appConfig } from "../../app.config";
import {
  HttpTestingController,
  provideHttpClientTesting,
} from "@angular/common/http/testing";
import { UserService } from "../../services/user/user.service";

describe("HomePageComponent", () => {
  let component: HomePageComponent;
  let fixture: ComponentFixture<HomePageComponent>;
  let userService: UserService;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HomePageComponent],
      providers: [provideHttpClientTesting(), ...appConfig.providers],
    }).compileComponents();

    fixture = TestBed.createComponent(HomePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    userService = TestBed.inject(UserService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
