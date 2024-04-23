import { makeAutoObservable } from "mobx";
import { ProfilePost } from "../types/commonTypes";
import StateStore from "./StateStores/StateStore";
import FetchingStateStore from "./StateStores/FetchingStateStore";
import ProfileServices from "../services/ProfileServices";
import SuccessStateStore from "./StateStores/SuccessStateStore";
import ErrorStateStore from "./StateStores/ErrorStateStore";

class FeedStore {
  post: ProfilePost[];
  state?: StateStore;

  constructor() {
    makeAutoObservable(this);
    this.post = [];
  }

  public async getPosts(pageSize: number, page: number) {
    this.state = new FetchingStateStore();
    try {
      const response = await ProfileServices.GetFeed(page, pageSize);
      if (response) {
        this.post = response.feedPostDto;
        const photoPromises = this.post.map(async (post) => {
          post.photoUrls = await Promise.all(post.photoIds.map(async (photo) => {
            return await ProfileServices.GetPostImage(post.profileId!, photo)
          }));
          return post;
        });
        await Promise.all(photoPromises);
        this.state = new SuccessStateStore();
      }
    } catch (error) {
      this.state = new ErrorStateStore();
      console.warn(error)
    }
  }
}

export default FeedStore;