import HomeHead from './home.head';
import HomeListCourses from './home.list-courses';
import HomeSearch from './home.search';
import { useGetListCoursesQuery } from '../../api/courses.api';

export default function HomePage() {
  const { data, isFetching } = useGetListCoursesQuery();

  return (
    <>
      <HomeHead />
      <HomeSearch />
      <HomeListCourses isLoading={isFetching} courses={data} />
    </>
  );
}
