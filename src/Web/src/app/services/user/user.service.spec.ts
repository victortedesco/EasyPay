import { TestBed } from "@angular/core/testing";

import { UserService } from "./user.service";
import { provideHttpClient, withFetch } from "@angular/common/http";
import {
  HttpTestingController,
  provideHttpClientTesting,
} from "@angular/common/http/testing";
import { User } from "../../models/user.model";

describe("UserService", () => {
  let service: UserService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(withFetch()), provideHttpClientTesting()],
    });
    service = TestBed.inject(UserService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it("should be created", () => {
    expect(service).toBeTruthy();
  });

  describe("getById", () => {
    it("should return user when user exists", () => {
      const id: number = 123456;

      const mockUser: User = {
        id: id,
        document: "Document 1",
        name: "Name 1",
        email: "user1@easypay.com",
        balance: 0,
      };

      service.getById(id).subscribe((receivedUser) => {
        expect(receivedUser).toEqual(mockUser);
      });

      const req = httpMock.expectOne(`${service["url"]}/id/${id}`);
      expect(req.request.method).toBe("GET");

      req.flush(mockUser);
    });

    it("should return not found when user does not exists", () => {
      const id: number = 123456;

      service.getById(id).subscribe({
        error: (error) => {
          expect(error.status).toBe(404);
          expect(error.statusText).toBe("Not Found");
        },
      });

      const req = httpMock.expectOne(`${service["url"]}/id/${id}`);
      expect(req.request.method).toBe("GET");

      req.flush(null, { status: 404, statusText: "Not Found" });
    });
  });

  describe("getByDocument", () => {
    it("should return user when user exists", () => {
      const document: string = "123456789";

      const mockUser: User = {
        id: 123456,
        document: document,
        name: "Name 1",
        email: "user1@easypay.com",
        balance: 0,
      };

      service.getByDocument(document).subscribe((receivedUser) => {
        expect(receivedUser).toEqual(mockUser);
      });

      const req = httpMock.expectOne(`${service["url"]}/document/${document}`);
      expect(req.request.method).toBe("GET");

      req.flush(mockUser);
    });

    it("should return not found when user does not exists", () => {
        const document: string = "123456789";

      service.getByDocument(document).subscribe({
        error: (error) => {
          expect(error.status).toBe(404);
          expect(error.statusText).toBe("Not Found");
        },
      });

      const req = httpMock.expectOne(`${service["url"]}/document/${document}`);
      expect(req.request.method).toBe("GET");

      req.flush(null, { status: 404, statusText: "Not Found" });
    });
  });
});
