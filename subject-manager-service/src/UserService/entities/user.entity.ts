import {
    BeforeInsert,
    Column,
    Entity, Index,
    PrimaryGeneratedColumn,
} from 'typeorm';
import {UUID} from "../../Utils/Types";

export enum UserRole {
    STUDENT,
    ADMIN
}

@Entity({
    name: 'users',
})
export class UserEntity {
    @PrimaryGeneratedColumn("uuid")
    id: UUID;
    @BeforeInsert() genarate(){ this.id=crypto.randomUUID()}

    @Column({ unique: true})
    @Index()
    username: string;

    @Column()
    password: string;

    @Column({
        type: "enum",
        enum: UserRole,
        default: UserRole.STUDENT
    })
    role: UserRole;

    @Column()
    identifier: string;
}