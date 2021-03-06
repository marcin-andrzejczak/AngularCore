import { Post } from '@app/models/post';
import { Image } from '@app/models/image';
import { Comment } from '@app/models/comment';

export class User {

  constructor(
    public id: string,
    public email: string,
    public password: string,
    public firstName: string,
    public lastName: string,
    public isAdmin: boolean,
    public profilePictureUrl: string,
    public images: Image[],
    public comments: Comment[],
    public friends: User[],
    public posts: Post[],
    public createdAt: Date,
    public modifiedAt: Date
  ){}

}
