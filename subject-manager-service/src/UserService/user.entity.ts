import {
    BeforeInsert,
    Column,
    CreateDateColumn,
    DeleteDateColumn,
    Entity, Index,
    PrimaryGeneratedColumn,
    UpdateDateColumn,
} from 'typeorm';

export enum UserRole {
    NORMAL,
    ADMIN
}

@Entity({
    name: 'users',
})
export class UserEntity {
    @PrimaryGeneratedColumn("uuid")
    id: `${string}-${string}-${string}-${string}-${string}`;
    @BeforeInsert() genarate(){ this.id=crypto.randomUUID()}

    @Column({ unique: true})
    @Index()
    username: string;

    @Column()
    password: string;

    @Column({
        type: "enum",
        enum: UserRole,
        default: UserRole.NORMAL
    })
    role: UserRole;

    @Column()
    identifier: string;
}