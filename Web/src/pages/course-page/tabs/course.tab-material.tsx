import { Button, Space, Typography } from 'antd';
import dayjs from 'dayjs';
import { useTranslation } from 'react-i18next';
import { Link, useParams } from 'react-router-dom';

import { TextEditorOutput } from '../../../common/typography';
import { CourseMaterial } from '../../../models/course-material.type';
import {
  CourseMaterialFileIcon,
  CourseMaterialIcon,
  CourseMaterialType,
  useDownloadCourseMaterialFile,
} from '../../../shared';
import { DATE_FORMAT } from '../../../utils/constants/app.constants';

interface Props {
  material: CourseMaterial;
}

export default function CourseTabMaterial({ material }: Props) {
  const { courseId } = useParams();

  const { t } = useTranslation();
  const { downloadCourseMaterialFile } = useDownloadCourseMaterialFile();

  switch (material.type) {
    case CourseMaterialType.Content:
      return <TextEditorOutput text={material.content} />;
    case CourseMaterialType.Link:
      return (
        <Link className="d-block" to={material.link} target="_blank">
          <Space align="start">
            <CourseMaterialIcon type={CourseMaterialType.Link} />
            {material.title}
          </Space>
        </Link>
      );
    case CourseMaterialType.File:
      return (
        <Button
          className="d-block p-0"
          type="link"
          icon={<CourseMaterialFileIcon extension={material.fileName} />}
          onClick={() =>
            downloadCourseMaterialFile(
              material.uniqueFileName,
              material.fileName,
            )
          }
        >
          {material.title}
        </Button>
      );
    case CourseMaterialType.Assignment:
      return (
        <div className="mt-field">
          <Link
            className="d-block"
            to={`/courses/${courseId}/assignment/${material.id}`}
          >
            <Space align="start">
              <CourseMaterialIcon type={CourseMaterialType.Assignment} />
              {material.title}
            </Space>
          </Link>
          {material.deadline && (
            <Typography.Paragraph className="m-0" type="secondary">
              {t('course_material.deadline')}:{' '}
              {dayjs(material.deadline).format(DATE_FORMAT)}
            </Typography.Paragraph>
          )}
        </div>
      );
    case CourseMaterialType.Test:
      return (
        <div className="mt-field">
          <Link
            className="d-block"
            to={`/courses/${courseId}/test/${material.id}`}
          >
            <Space align="start">
              <CourseMaterialIcon type={CourseMaterialType.Test} />
              {material.title}
            </Space>
          </Link>
          {material.deadline && (
            <Typography.Paragraph className="m-0" type="secondary">
              {t('course_test.deadline')}:{' '}
              {dayjs(material.deadline).format(DATE_FORMAT)}
            </Typography.Paragraph>
          )}
        </div>
      );
  }
}
