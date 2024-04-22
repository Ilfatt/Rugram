import { Posts, ProfileRequest, SearchProfile } from "../types/commonTypes";
import { ApiConnection } from "./ApiConnection";

class ProfileServices {
  static async Follow(id: string) {
    const response = await ApiConnection.put(`subscribe/${id}`);
    return response;
  }

  static async UnFollow(id: string) {
    const response = await ApiConnection.put(`unsubscribe/${id}`);
    return response;
  }

  static async GetProfile(id: string) {
    const name = await ApiConnection.get<{profileName: string}>(`profile/profileName/${id}`);
    const icon = (await ApiConnection.get<{photo: string}>(`profile/profilePhoto/${id}`))
    const followers = await ApiConnection.get<{
      subscribersCount: number,
      subscriptionsCount: number
    }>(`profile/profileIndicators/${id}`);

    return { ...name.data, icon , ...followers.data } as unknown as ProfileRequest;
  }

  static async GetPosts(id: string, pageNumber: number, pageSize: number) {
    const response = await ApiConnection.get<Posts>(`post/${id}&${pageNumber}&${pageSize}`);
    return response.data;
  }

  static async GetPostImage(id: string, photoId: string) {
    const response = await ApiConnection.get<{photo: string}>(`post/photo/${photoId}&${id}`);
    return response.data.photo;
  }

  static async Search(search: string) {
    const response = await ApiConnection.get<SearchProfile>(`profile/recommendations/${search}&8&1`);
    return response.data;

  }
}

export default ProfileServices;
