import { useState } from 'react';

import HomeCourseList from './home.course-list';
import HomeHead from './home.head';
import HomeSearch from './home.search';

export default function HomePage() {
  const [search, setSearch] = useState<string>();

  return (
    <>
      <HomeHead />
      <HomeSearch onSearch={setSearch} />
      <HomeCourseList search={search} />
    </>
  );
}
