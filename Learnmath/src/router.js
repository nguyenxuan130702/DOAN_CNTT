import { createRouter, createWebHistory } from 'vue-router'

import Login from './components/LoginPage.vue'
import Register from './components/RegisterUser.vue'
import Home from './components/HomePage.vue'
import News from './components/NewsPage.vue'
import DetailNews from './components/DetailNews.vue'
import Profile from './components/ProfileUser.vue'
import Checkout from './components/CheckoutPage.vue'
import notfound from './components/layout/V404Page.vue'
import Radio from './components/RadioPage.vue'
import Course from './components/CoursePage.vue'
import SettingProfile from './components/SettingProfile.vue'
import Lesson from './components/LessonPage.vue'
import Question from './components/QuestionPage.vue'
import Overview from './components/admin/OverView.vue'
import Forum from './components/ForumPage.vue'
import Category from './components/CategoriesPage.vue'
import DetailThread from './components/DetailThreads.vue'
import Start from './components/StartPage.vue'
import Exam from './components/ExamPage.vue'
import DetailExam from './components/DetailExam.vue'
import VideoCourse from './components/VideoCourse.vue'
import HomeWork from './components/HomeWork.vue'
import DetailVideo from './components/DetailVideo.vue'
import MenuCourse from './components/admin/MenuCourse.vue'
import MenuQuestion from './components/admin/MenuQuestion.vue'
import QuestionVideo from './components/admin/QuestionVideo.vue'

import TeacherCourse from './components/teacher_course/DetailTeacherCourse.vue'
import LearnCourse from './components/teacher_course/LearnCourse.vue'
import HomeTeacher from './components/teacher/HomeTeacher.vue'
import CourseTeacher from './components/teacher/quanly/CourseTeacher.vue'
import DocumentTeacher from './components/teacher/quanly/DocumentTeacher.vue'
import DanhGia from './components/teacher/quanly/DanhGia.vue'


import HomeAdmin from './components/admin/layout/HomeAdmin.vue'
import CourseAdmin from './components/admin/CourseAdmin.vue'
import LessonAdmin from './components/admin/LessonAdmin.vue'
import QuestionAdmin from './components/admin/QuestionAdmin.vue'
import NewsAdmin from './components/admin/NewsAdmin.vue'
import ProfileAdmin from './components/admin/ProfileAdmin.vue'
import UserAdmin from './components/admin/UserAdmin.vue'
import CategoryAdmin from './components/admin/CategoryAdmin.vue'
import ThreadAdmin from './components/admin/ThreadAdmin.vue'
import CommentAdmin from './components/admin/CommentAdmin.vue'
import VideoAdmin from './components/admin/VideoAdmin.vue'
import ScorePage from './components/KetQua.vue'
import CourseVideo from './components/admin/CourseVideo.vue'
import ScoreAdmin from './components/admin/ScorePage.vue'
import LoginTeacher from './components/teacher/LoginTeacher.vue'
import ProfileTeacher from './components/teacher/quanly/hoso/HoSoTeacher.vue'
import GiaoVien from './components/admin/giaovien/QLGiaoVien.vue'


const routes = [

    { path: '/', component: Home, name: 'home' },
    { path: '/start', component: Start, name: 'start'},
    { path: '/Login', component: Login, name: 'login' },
    { path: '/Register', component: Register },
    { path: '/News', component: News },
    { path: '/News/:id', component: DetailNews },
    { path: '/Profile', component: Profile, name: 'Account' },
    { path: '/Checkout', component: Checkout },
    { path: '/notfoud', component: notfound, name: '404' }, 
    { path: '/Radio', component: Radio, name: 'radio'}, 
    { path: '/Course', component: Course, name: 'course'}, 
    { path: '/SettingProfile', component: SettingProfile, name: 'setting'}, 
    { path: '/Lesson/:id', component: Lesson, name: 'lesson'}, 
    { path: '/Question/:id', component: Question, name: 'question'}, 
    { path: '/Forum', component: Forum, name: 'forum'}, 
    { path: '/Category/:id', component: Category, name: 'category'}, 
    { path: '/DetailThread/:id', component: DetailThread, name: 'thread'}, 
    { path: '/Overview', component: Overview, name: 'overview'}, 
    { path: '/HomeAdmin', component: HomeAdmin, name: 'homeadmin'}, 
    { path: '/CourseAdmin', component: CourseAdmin, name: 'courseadmin'}, 
    { path: '/LessonAdmin', component: LessonAdmin, name: 'lessonadmin'}, 
    { path: '/QuestionAdmin', component: QuestionAdmin, name: "questionadmin"}, 
    { path: '/NewAdmin', component: NewsAdmin, name: "newadmin"}, 
    { path: '/ProfileAdmin', component: ProfileAdmin, name: "profileadmin"}, 
    { path: '/Useradmin', component: UserAdmin, name: "useradmin"}, 
    { path: '/CategoryAdmin', component: CategoryAdmin, name: "categoryadmin"}, 
    { path: '/ThreadAdmin', component: ThreadAdmin, name: "threadadmin"}, 
    { path: '/CommentAdmin', component: CommentAdmin, name: "commentadmin"}, 
    { path: '/VideoAdmin', component: VideoAdmin, name: "videoadmin"}, 
    { path: '/ExamPage', component: Exam, name: "exampage"}, 
    { path: '/DetailExam', component: DetailExam, name: "detailexam"}, 
    { path: '/ScorePage', component: ScorePage, name: 'scorepage'},
    { path: '/VideoCourse', component: VideoCourse, name: "videocourse"},
    { path: '/HomeWork', component: HomeWork, name: "homework"}, 
    { path: '/DetailVideo', component: DetailVideo, name: "detailvideo"}, 
    { path: '/MenuCourse', component: MenuCourse, name: "menucourse"},
    { path: '/CourseVideo', component: CourseVideo, name: "coursevideo"}, 
    { path: '/MenuQuestion', component: MenuQuestion, name: "menuquestion"}, 
    { path: '/QuestionVideo', component: QuestionVideo, name: "questionvideo"}, 
    { path: '/ScoreAdmin', component: ScoreAdmin, name: "scoreadmin"}, 
    { path: '/TeacherCourse', component: TeacherCourse, name: "teachercourse"}, 
    { path: '/LearnCourse', component: LearnCourse, name: "learncourse"}, 
    { path: '/LoginTeacher', component: LoginTeacher, name: "loginteacher"}, 
    { path: '/HomeTeacher', component: HomeTeacher, name: "hometeacher"}, 
    { path: '/CourseTeacher', component: CourseTeacher, name: "courseteacher"}, 
    { path: '/DocumentTeacher', component: DocumentTeacher, name: 'documentteacher'}, 
    { path: '/DanhGia', component: DanhGia, name: 'danhgia'}, 
    { path: '/ProfileTeacher', component: ProfileTeacher, name: 'profileteacher'}, 
    { path: '/GiaoVien', component: GiaoVien, name: 'giaovien'}
]

const router = createRouter({
    history: createWebHistory(),
    routes: routes
})

export default router; 