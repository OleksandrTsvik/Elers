import { useState } from 'react';

import CourseEditBreadcrumb from './course-edit.breadcrumb';
import CourseEditDescription from './course-edit.description';
import CourseEditHead from './course-edit.head';
import CourseEditTitle from './course-edit.title';
import { Course } from '../../models/course.interface';

interface Props {
  course: Course;
}

export default function CourseEditContent({ course }: Props) {
  const [title, setTitle] = useState(course.title);
  const [description, setDescription] = useState(course.description);

  return (
    <>
      <CourseEditHead title={title} />
      <CourseEditBreadcrumb courseId={course.id} title={title} />
      <CourseEditTitle
        courseId={course.id}
        title={title}
        onUpdateTitle={setTitle}
      />
      <CourseEditDescription
        courseId={course.id}
        description={description}
        onUpdateDescription={setDescription}
      />
    </>
  );
}
