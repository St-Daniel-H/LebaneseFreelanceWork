import Notifications from "./Home/@Notifications/page";
import TansackProvider from "./Components/Providers/QueryProvider";
export const metadata = {
  title: "Next.js",
  description: "Generated by Next.js",
};
type Types = {
  children: React.ReactNode;
};
export default function RootLayout({ children }: Types) {
  return (
    <html lang="en">
      <body>
        <TansackProvider> {children}</TansackProvider>
      </body>
    </html>
  );
}
