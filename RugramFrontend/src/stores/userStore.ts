import { makeAutoObservable, runInAction } from "mobx";
import Authorization from "../services/AuthServices";
import StateStore from "./StateStores/StateStore";
import FetchingStateStore from "./StateStores/FetchingStateStore";
import ErrorStateStore from "./StateStores/ErrorStateStore";
import SuccessStateStore from "./StateStores/SuccessStateStore";
import { decodeToken } from "../tools/decodeToken";
import ProfileServices from "../services/ProfileServices";
import { User } from "../types/commonTypes";

class UserStore {
  user: User;

  state?: StateStore;

  token?: string;

  refreshToken?: string;

  constructor() {
    makeAutoObservable(this);
    this.token = localStorage.getItem("rugramToken") ?? undefined;
    this.refreshToken = localStorage.getItem("refreshToken") ?? undefined;
    this.user = {
      id: undefined,
      username: undefined,
      img: undefined,
      followersCount: undefined,
      followingCount: undefined,
      description: undefined,
    };
    this.user.id = this.token ? decodeToken(this.token) : undefined;
  }

  public async SendEmail(email: string) {
    this.state = new FetchingStateStore();
    try {
      await Authorization.SendEmail(email);
      this.state = new SuccessStateStore();
      return true;
    } catch (error) {
      runInAction(() => {
        this.state = new ErrorStateStore(error);
      });
    }
  }

  public async CheckProfileName(profileName: string) {
    this.state = new FetchingStateStore();
    try {
      await Authorization.checkNameAvailability(profileName);
      this.state = new SuccessStateStore();
      return true;
    } catch (error) {
      runInAction(() => {
        this.state = new ErrorStateStore(error);
      });
    }
  }

  public async CheckMail(mail: string) {
    this.state = new FetchingStateStore();
    try {
      this.state = new SuccessStateStore();
      return await Authorization.checkMailAvailability(mail);
    } catch (error) {
      runInAction(() => {
        this.state = new ErrorStateStore(error);
      });
    }
  }

  public async Registration(
    token: string,
    email: string,
    password: string,
    profileName: string,
  ) {
    this.state = new FetchingStateStore();
    try {
      const response = await Authorization.Registration(
        token,
        email,
        password,
        profileName,
      );
      if (response) {
        localStorage.setItem("rugramToken", response.jwtToken);
        localStorage.setItem("refreshToken", response.refreshToken);
        this.token = response.jwtToken;
        this.refreshToken = response.refreshToken;
        this.state = new SuccessStateStore();
      }
    } catch (error) {
      runInAction(() => {
        this.state = new ErrorStateStore(error);
      });
    }
  }

  public async Login(email: string, password: string) {
    this.state = new FetchingStateStore();
    try {
      const response = await Authorization.Login(email, password);
      if (response) {
        localStorage.setItem("rugramToken", response.jwtToken);
        localStorage.setItem("refreshToken", response.refreshToken);
        this.token = response.jwtToken;
        this.refreshToken = response.refreshToken;
        this.state = new SuccessStateStore();
      }
    } catch (error) {
      runInAction(() => {
        this.state = new ErrorStateStore(error);
      });
    }
  }

  public LogOut() {
    this.token = undefined;
    this.refreshToken = undefined;
    localStorage.removeItem("rugramToken");
    localStorage.removeItem("refreshToken");
  }

  public async updateJwt() {
    this.state = new FetchingStateStore();
    try {
      const response = await Authorization.updateToken(this.token!, this.refreshToken!);
      if (response) {
        localStorage.setItem("rugramToken", response.jwtToken);
        this.token = response.jwtToken;
        this.state = new SuccessStateStore();
      }
    } catch (error) {
      runInAction(() => {
        this.state = new ErrorStateStore(error);
      });
    }
  }

  public async follow(id: string) {
    this.state = new FetchingStateStore();
    try {
      await ProfileServices.Follow(id);
      this.state = new SuccessStateStore();
    } catch (error) {
      runInAction(() => {
        this.state = new ErrorStateStore(error);
      });
    }
  }

  public async unFollow(id: string) {
    this.state = new FetchingStateStore();
    try {
      await ProfileServices.UnFollow(id);
      this.state = new SuccessStateStore();
    } catch (error) {
      runInAction(() => {
        this.state = new ErrorStateStore(error);
      });
    }
  }

  public async getProfile(id: string) {
    this.state = new FetchingStateStore();
    try {
      const response = await ProfileServices.GetProfile(id);
      this.user.username = response.profileName
      this.user.img = response.icon;
      this.user.followersCount = response.subscribersCount;
      this.user.followingCount = response.subscriptionsCount;
      // this.profileDescription = response.profileDescription;
      this.state = new SuccessStateStore();
    } catch (error) {
      runInAction(() => {
        this.state = new ErrorStateStore(error);
      });
    }
  }

  public async clearUser() {
    this.user.username = undefined;
    this.user.followersCount = undefined;
    this.user.followingCount = undefined;
    this.user.description = undefined;
    this.user.img = undefined;
  }
}

export default UserStore;
