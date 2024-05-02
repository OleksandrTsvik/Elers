import { List } from 'antd';

import HomeListCourseItem from './home.list-course-item';
import { Course } from '../../models/course.interface';

interface Props {
  courses?: Course[];
  isLoading: boolean;
}

export default function HomeListCourses({ courses, isLoading }: Props) {
  return (
    <List
      size="large"
      itemLayout="vertical"
      loading={isLoading}
      dataSource={courses}
      pagination={{ pageSize: 4 }}
      renderItem={(item) => <HomeListCourseItem courseItem={item} />}
    />
  );
}
