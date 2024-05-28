import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useUpdateCourseMaterialContentMutation } from '../../api/course-materials.mutations.api';
import {
  CourseTabType,
  MaterialContentEditor,
  getCourseEditPagePath,
} from '../../shared';

interface Props {
  courseId: string;
  courseMaterialId: string;
  tabId: string;
  courseTabType: CourseTabType;
  content: string;
}

export default function MaterialContentEditEditor({
  courseId,
  courseMaterialId,
  tabId,
  courseTabType,
  content,
}: Props) {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const [updateCourseMaterial, { isLoading, error }] =
    useUpdateCourseMaterialContentMutation();

  const handleSubmit = async (content: string) => {
    await updateCourseMaterial({ tabId, id: courseMaterialId, content })
      .unwrap()
      .then(() =>
        navigate(getCourseEditPagePath(courseId, tabId, courseTabType)),
      );
  };

  return (
    <MaterialContentEditor
      initialText={content}
      textOnSubmitButton={t('actions.save_changes')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
